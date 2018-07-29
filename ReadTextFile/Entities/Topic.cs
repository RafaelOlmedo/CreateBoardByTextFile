using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadTextFile.Entities
{
    public class Topic
    {
        public string Descricao { get; set; }
        public string Label { get; set; }
        public string Level { get; set; }
        public decimal TimeDevelopment { get; set; }
        public decimal TimeTest { get; set; }



        public List<SubTopic2> Topics { get; set; }

        public Topic()
        {
            //this.Descricao = Descricao;
        }

        /// <summary>
        /// Recebe a linha de nível 1 da Hierarquia da estimativa e quebra as informações para preencher os atributos.
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        public Topic explodesLevelOneInformations(Topic topic, string line)
        {
            var lineSplitBySpace = line.Split(' ');

            string sTime = string.Empty;
            decimal dTime = 0.0m;
            for (int iC = 0; iC < lineSplitBySpace.Length; iC++)
            {
                // Recupera o nível do tópico
                if (lineSplitBySpace[iC].ToString() == "_(Nível")
                    topic.Level = lineSplitBySpace[iC + 1].Replace(")_", "");
                else if (lineSplitBySpace[iC].ToString() == "*%{background:lightgreen}[D]")
                {
                    sTime = lineSplitBySpace[iC + 1].Replace("h%", "").Replace(".", ",");

                    if (decimal.TryParse(sTime, out dTime))
                        topic.TimeDevelopment = dTime;

                    // TODO = Caso não seja número, realizar tratamento.

                }
                else if (lineSplitBySpace[iC].ToString() == "%{background:yellow}[T]")
                {
                    // Informações referente ao tempo de testes.
                    sTime = lineSplitBySpace[iC + 1].Replace("h%:*", "").Replace(".", ",");

                    if (decimal.TryParse(sTime, out dTime))
                        topic.TimeTest = dTime;

                    // TODO = Caso não seja número, realizar tratamento.

                    #region Tratamento para a descrição do tópico.
                    for (int iT = iC + 2; iT < lineSplitBySpace.Length; iT++)
                    {
                        topic.Descricao += lineSplitBySpace[iT].ToString() + " ";
                    }

                    #endregion

                }
            }

            return topic;

        }
    }
}
