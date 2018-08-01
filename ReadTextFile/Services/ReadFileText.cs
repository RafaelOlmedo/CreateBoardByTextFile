using Newtonsoft.Json;
using ReadTextFile.Entities;
using ReadTextFile.Entities.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadTextFile.Services
{
    public class ReadFileText
    {
        public static List<Topic> readTextFileToObject(ConfigProperties configProperties)
        {
            // Realiza a leitura do arquivo e cada linha vira um indice o array.
            string[] lines = System.IO.File.ReadAllLines(configProperties.FilePath + configProperties.FileName);

            // Lista de StringBuilder. Cada índice da lista será um tópico.
            List<StringBuilder> stringBuilders = new List<StringBuilder>();

            // Controle do índice atual para preecher a lista de stringbuilder
            int iIndexTopic = -1;

            #region Percorre o array com as linhas do arquivo para separar os tópicos na lista de stringbuilder.

            foreach (var line in lines)
            {
                if (line.StartsWith("# "))
                {
                    stringBuilders.Add(new StringBuilder().Append(line.ToString()));
                    iIndexTopic++;
                }
                else if (!string.IsNullOrEmpty(line.ToString()))
                {
                    // Incluído "AppendLine" para existir um delimitador e depois separar cada linha do tópico.
                    stringBuilders[iIndexTopic].AppendLine();
                    stringBuilders[iIndexTopic].Append(line.ToString());
                }
            }

            #endregion            

            // Lista para armazenar a entidade Topic com todos os tópicos presentes no arquivo.
            List<Topic> lstAllTopics = new List<Topic>();

            // Percorre cada tópico existente na lista de stringbuilder.
            foreach (var topic in stringBuilders)
            {
                // Realiza o split por '/r/n' para cada posição do array ser uma linha do arquivo.
                var topicLines = topic.
                    ToString().
                    Split(new string[] { "\r\n" }, StringSplitOptions.None);

                Topic allTopic = new Topic();
                List<SubTopic2> lstSubTopic2 = new List<SubTopic2>();
                SubTopic2 subTopic2 = new SubTopic2();
                List<SubTopic3> lstSubTopic3 = new List<SubTopic3>();
                SubTopic3 subTopic3 = new SubTopic3();
                List<SubTopic4> lstSubTopic4 = new List<SubTopic4>();
                SubTopic4 subTopic4 = new SubTopic4();

                // Variável de controle para saber qual o nível na hierarquia da linha acima.
                int iLastLevel = 0;

                // Instancia objeto 'Pai' para armazenar as informações.
                allTopic = new Topic();

                // Percorre todas as linhas de determinado tópico para preencher o objeto respeitando a hierarquia das informações.
                foreach (var topicLine in topicLines)
                {
                    // Primeira linha da hierarquia.
                    if (topicLine.StartsWith("# "))
                    {
                        allTopic.explodesLevelOneInformations(allTopic, topicLine);

                        if (!allTopic.validationResults.IsValid)
                        {
                            lstAllTopics.Add(allTopic);
                            return lstAllTopics;
                        }


                        iLastLevel = 1;
                    }
                    // Segunda linha da hierarquia.
                    else if (topicLine.StartsWith("## "))
                    {
                        // Caso a última linha tenha sido nível 2, indica que a última linha não possui filhos, portanto adiciona a última
                        // linha à lista.
                        if (iLastLevel == 2)
                            lstSubTopic2.Add(subTopic2);
                        // Caso a última linha tenha sido nível 3, adiciona a última linha à lista de tópicos 3 e adiciona essa lista
                        // ao último índice da lista de tópico 2.
                        else if (iLastLevel == 3)
                        {
                            lstSubTopic3.Add(subTopic3);
                            lstSubTopic2[lstSubTopic2.Count - 1].Topics = lstSubTopic3;
                        }
                        // Caso a última linha tenha sido nível 4, adiciona a última linha à lista de tópicos  e adiciona essa lista
                        // ao último índice da lista de tópico 3.
                        else if (iLastLevel == 4)
                        {
                            lstSubTopic4.Add(subTopic4);
                            lstSubTopic3[lstSubTopic3.Count - 1].Topics = lstSubTopic4;

                            // Adiciona a lista de tópico 2 as informações do tópico 3.
                            lstSubTopic2[lstSubTopic2.Count - 1].Topics = lstSubTopic3;
                        }

                        // Adiciona o tópico nível 2 atual ao objeto.
                        subTopic2 = new SubTopic2(topicLine.ToString());
                        iLastLevel = 2;


                    }
                    // Terceira linha da hierarquia.
                    else if (topicLine.StartsWith("### "))
                    {
                        // Caso a última linha tenha sido nível 3, indica que a última linha não possui filhos, portanto adiciona a última
                        // linha à lista.
                        if (iLastLevel == 3)
                            lstSubTopic3.Add(subTopic3);
                        // Caso a última linha tenha sido nível 2, indica que a última linha não possui filhos, portanto adiciona a última
                        // linha à lista.
                        else if (iLastLevel == 2)
                            lstSubTopic2.Add(subTopic2);
                        // Caso a última linha tenha sido nível 4, adiciona a última linha à lista de tópicos  e adiciona essa lista
                        // ao último índice da lista de tópico 3.
                        else if (iLastLevel == 4)
                        {
                            lstSubTopic4.Add(subTopic4);
                            lstSubTopic3[lstSubTopic3.Count - 1].Topics = lstSubTopic4;
                        }

                        // Caso a última linha seja diferente de 3, indica que essa linha é filha de um subtópico diferente dos anteriores,
                        // portanto necessário limpar a lista.
                        if (iLastLevel < 3)
                            lstSubTopic3 = new List<SubTopic3>();

                        // Adiciona o tópico nível 3 atual ao objeto.
                        subTopic3 = new SubTopic3(topicLine.ToString());
                        iLastLevel = 3;
                    }
                    // Quarta linha da hierarquia.
                    else if (topicLine.StartsWith("#### "))
                    {
                        if (iLastLevel == 4)
                            lstSubTopic4.Add(subTopic4);
                        else if (iLastLevel == 3)
                            lstSubTopic3.Add(subTopic3);

                        if (iLastLevel < 4)
                            lstSubTopic4 = new List<SubTopic4>();

                        subTopic4 = new SubTopic4(topicLine.ToString());
                        iLastLevel = 4;
                    }
                }

                // Caso a última linha tenha sido do nível 2, necessário incluir na lista dos tópicos 2.
                if (iLastLevel == 2)
                    lstSubTopic2.Add(subTopic2);
                // Caso a última linha tenha sido do nível 3, necessário incluir na lista dos tópicos 3.
                // Necessário também adicionar a lista do tópicos 3 no último indice da lista dos tópicos 2.
                else if (iLastLevel == 3)
                {
                    lstSubTopic3.Add(subTopic3);
                    lstSubTopic2[lstSubTopic2.Count - 1].Topics = lstSubTopic3;
                }
                // Caso a última linha tenha sido do nível 4, necessário incluir na lista dos tópicos 4.
                // Necessário também adicionar a lista do tópicos 4 no último indice da lista dos tópicos 3.
                // Necessário também adicionar a lista do tópicos 3 no último indice da lista dos tópicos 2.

                else if (iLastLevel == 4)
                {
                    lstSubTopic4.Add(subTopic4);
                    lstSubTopic3[lstSubTopic3.Count - 1].Topics = lstSubTopic4;
                    lstSubTopic2[lstSubTopic2.Count - 1].Topics = lstSubTopic3;
                }

                // Sempre é necessário adicionar a lista dos tópicos 2 na objeto do tópico.
                allTopic.Topics = lstSubTopic2;

                // Adiciona o tópico atual na lista de tópicos.
                lstAllTopics.Add(allTopic);

            }

            writeJSONFile(lstAllTopics, configProperties);

            return lstAllTopics;

        }

        /// <summary>
        /// Serializa o objeto para json e salva.
        /// TODO: Receber uma entidade genérica.
        /// </summary>
        /// <param name="lstAllTopics"></param>
        /// <param name="configProperties"></param>
        public static void writeJSONFile(List<Topic> lstAllTopics, ConfigProperties configProperties)
        {
            // Serializa o objeto para json.
            // (SOMENTE POR TESTE PARA VER COMO É)
            var jsonString = JsonConvert.SerializeObject(lstAllTopics);

            // Salva arquivo Json.
            System.IO.File.WriteAllText(configProperties.FilePath + "JSONTest.json", jsonString.ToString());
        }      
    }
}
