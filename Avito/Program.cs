using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avito
{
    class Program
    {
        public static List<IValue> ReadFromValue()
        {
            List<IValue> values = new List<IValue>();
            string json = File.ReadAllText("Values.json", Encoding.GetEncoding(1251));

            // Parse JSON into dynamic object, convenient!
            JObject parameters = JObject.Parse(json);

            // Process each employee
            foreach (var value in parameters["values"])
            {
                // this can be a string or null

                // this can be a string or array, how can we tell which it is
                JToken Value = value["value"];
                try
                {
                    int id = (int)value["id"];
                    ValueInt v = new ValueInt { Id = id };
                    var valueName = (int)Value;
                    v.intvalue = valueName;
                    values.Add(v);
                }
                catch
                {
                    int id = (int)value["id"];
                    ValueString v = new ValueString { Id = id };
                    var valueName = (string)Value;
                    v.stringvalue = valueName;
                    values.Add(v);
                }                             
                                   
            }
            return values;
            
        }

        public static ValueInt ReadFromTestcaseStructure()
        {
            ValueInt Params = new ValueInt();
            if (File.Exists("TestcaseStructure.json"))
            {
                string json = File.ReadAllText("TestcaseStructure.json");
                Params = JsonConvert.DeserializeObject<ValueInt>(json);
            }
            return Params;
        }

        public static void ValueSelect(List<Param> Params, List<IValue> Values)
        {
            for (int i = 0; i < Params.Count(); i++) //заходим в список параметров
            {
                for (int j = 0; j < Values.Count(); j++) //заходим в список значений файла Values.json
                {

                    if (Params[i].Id == Values[j].Id) //условия нахождения пар по равенству id
                    {
                        if (Params[i].Values != null) // если есть варианты значений (values) в параметре
                        {
                            for (int k = 0; k < Params[i].Values.Count(); k++) //запускаем цикл по данным вариантам значений
                            {
                                ValueInt v = Values[j] as ValueInt;

                                if (Params[i].Values[k].Id == v.intvalue) //если id одного из вариантов совпадает со значением в Values.json
                                {
                                    Params[i].SelectedValue = Params[i].Values[k].Title; //выбираем название выбранного значения и присваеваем SelectedValue
                                    break; //Конец цикла по возможным вариантам значений
                                }
                            }
                        }

                        else
                        {
                            try
                            {
                                ValueInt v = Values[j] as ValueInt;
                                Params[i].SelectedValue = v.intvalue;
                            }
                            catch (Exception)
                            {
                                ValueString v = Values[j] as ValueString;
                                Params[i].SelectedValue = v.stringvalue;
                            }

                        }
                    }
                }

                if (Params[i].SelectedValue == null)
                {
                    Params[i].SelectedValue = "";
                }
            }       
        }

        public static void Main(string[] args)
        {
            //Param values = ReadFromValue();
            //List<IValue> Values = values.Values.ToList();
            try
            {
                List<IValue> Values = ReadFromValue();

                ValueInt Params = ReadFromTestcaseStructure();
                List<Param> Parameters = Params.Params;                

                //заполняем сначала внутренние параметры
                for (int i = 0; i < Parameters.Count(); i++)//заходим в главный список
                {
                    if (Parameters[i].Values != null) //если есть варианты значений (values) в параметре
                    {                    
                        for (int k = 0; k < Parameters[i].Values.Count(); k++) //запускаем цикл по данным вариантам значений
                        {
                            if (Parameters[i].Values[k].Params != null) //если есть параметры у value
                            {
                                ValueSelect(Parameters[i].Values[k].Params, Values);
                            }
                        }
                    }               
                }

                ValueSelect(Parameters, Values);
                Params.Params = Parameters;               

                string json = JsonConvert.SerializeObject(Params, Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
                string filename = "StructureWithValues.json";
                string path = Directory.GetCurrentDirectory();
                if (!File.Exists(filename))
                {
                    File.Create(path + filename);
                }
                File.WriteAllText(filename, json);


            }
            catch
            {
                error NewError = new error("Входные файлы некорректны");
                string json = JsonConvert.SerializeObject(NewError, Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
                string filename = "error.json";
                string path = Directory.GetCurrentDirectory();
                if (!File.Exists(filename))
                {
                    File.Create(path + filename);
                }
                File.WriteAllText(filename, json);

            }
        }
    }
}