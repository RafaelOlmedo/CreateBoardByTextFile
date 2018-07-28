using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadTextFile
{
    public class JsonTest
    {
        public void test()
        {
            #region JSON TEST.   

            string sFilePath = @"C:\Users\rafael.freitas\Desktop\Projeto Estudo\";
            

            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;

                writer.WriteStartObject();

                writer.WritePropertyName("NumeroTarefa");
                writer.WriteValue("2650");

                writer.WritePropertyName("NomeTarefa");
                writer.WriteValue("Tarefa Teste");

                writer.WritePropertyName("Topics");
                writer.WriteStartArray();

                writer.WriteStartObject();
                writer.WritePropertyName("descricao");
                writer.WriteValue("BLABLABLABLA");

                writer.WritePropertyName("SubTopic2");
                writer.WriteStartArray();

                writer.WriteStartObject();
                writer.WritePropertyName("descricao");
                writer.WriteValue("BLABLABLABLABLA");

                writer.WritePropertyName("SubTopic3");
                writer.WriteStartArray();

                writer.WriteStartObject();
                writer.WritePropertyName("descricao");
                writer.WriteValue("BLABLABLABLABLABLABLABLABLABLA");

                writer.WriteEndArray();
                //writer.WriteEndArray();


                //writer.WriteStartArray();

                writer.WriteStartObject();
                writer.WritePropertyName("descricao");
                writer.WriteValue("BBBBBBBBBBBBBBBBBBBBBBBBB");

                writer.WritePropertyName("SubTopic2");
                writer.WriteStartArray();

                writer.WriteStartObject();
                writer.WritePropertyName("descricao");
                writer.WriteValue("BBBBBBBBBBBBBBBBBBBBBBBBB");

                writer.WritePropertyName("SubTopic3");
                writer.WriteStartArray();

                writer.WriteStartObject();
                writer.WritePropertyName("descricao");
                writer.WriteValue("BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB");

                writer.WriteEndArray();

                //writer.WriteStartObject();
                //writer.WritePropertyName("Type");
                //    writer.WriteStartObject();

                //    writer.WritePropertyName("Size");
                //    writer.WriteValue("700 gigabyte hard drive");

                //    writer.WritePropertyName("SizeNew");
                //    writer.WriteValue("AAAA");
                //    writer.WriteEndObject();


                writer.WriteEnd();
                // writer.WriteEndObject();
            }


            System.IO.File.WriteAllText(sFilePath + "JSONTest.json", sb.ToString());
            //Console.WriteLine(sb.ToString());
            //Console.WriteLine("Press any key to exit.");
            //System.Console.ReadKey();

            #endregion
        }

    }
}
