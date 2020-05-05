using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web;

namespace Empresa.MinhaApi.Api.Formatters
{
    public class CsvMediaTypeFormatter : BufferedMediaTypeFormatter
    {
        //ctor tap tap -- comando para criar construtor
        public CsvMediaTypeFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/csv"));
        }

        public override bool CanReadType(Type type) //Quando está querendo fazer uma leitura de uma request (ler neste formato)
        {
            return false; //Pode nunca fazer leitura
        }

        public override bool CanWriteType(Type type) //Quando está serializando as informações para dentro do response (escrever neste formato)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            return true;
        }

        //Onde ocorre a serialização de fato
        public override void WriteToStream(Type type, object value, Stream writeStream, HttpContent content)
        {
            using (StreamWriter writer = new StreamWriter(writeStream)) //criando um writer para dentro do response
            {
                IEnumerable enumerable = value as IEnumerable; //enumerable é nulo quando o value não é uma coleção
               
                if (enumerable == null){ //é um único objeto
                    writer.WriteLine(string.Join(";", GetPropertyNames(type))); //Pega tipos/atributos do meu DTO (ex.: AlunoDTO)
                    WriteElement(value, writer);
                } else
                {//É uma lista 
                    Type dtoType = value.GetType().GetGenericArguments()[0]; //Retorna um array de tipos genéricos 
                    writer.WriteLine(string.Join(";",GetPropertyNames(dtoType))); //Pega tipos/atributos do meu DTO (ex.: AlunoDTO)
                    foreach (var item in enumerable)
                    {
                        WriteElement(item, writer);
                    }
                }
            }

        }

        private IEnumerable<string> GetPropertyNames(Type type) //Propriedades do DTO
        {
            return type.GetProperties().Select(s => s.Name);
        }

        //Vai escrever o elemento no Stream de fato
        private void WriteElement(object item, StreamWriter writer)
        {
            string value = string.Empty;
            foreach(string property in GetPropertyNames(item.GetType()))
            {
                var propertyInfo = item.GetType().GetProperty(property);
                if (propertyInfo.GetIndexParameters().Length == 0)
                { //Se não houver indíce
                    var propertyValue = item.GetType().GetProperty(property).GetValue(item);
                    if (propertyValue != null)
                    {
                        value += propertyValue.ToString() + ";";
                    }
                }

                //value += ";";
            }
            writer.WriteLine(value.Substring(0, value.Length - 2));
        }
    }
}