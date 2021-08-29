using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Bungee_Tech_Internship_Test
{
    class source_code
    {
        static void Main(string[] args)
        {
            String input_file = @"D:\CvM\Downloads\internship-test-master\internship-test-master\input\main.csv";
            String output_path = @"D:\CvM\Downloads\internship-test-master\internship-test-master\output";
            String first_file = output_path + @"\filteredCountry.csv";
            String second_file = output_path + @"\lowestPrice.csv";
            if (!Directory.Exists(output_path))
            {
                Directory.CreateDirectory(output_path);
            }
            File.Create(first_file).Close();
            File.Create(second_file).Close();
            List<String> first_file_lines = new List<String>();
            List<String> second_file_lines = new List<String>();
            Dictionary<String, float[]> second_file_values = new Dictionary<String, float[]>();
            if (File.Exists(input_file))
            {
                String fline_first_file;
                String fline_second_file = "SKU,FIRST_MINIMUM_PRICE,SECOND_MINIMUM_PRICE";
                using (StreamReader reader = new StreamReader(input_file))
                {
                    fline_first_file = reader.ReadLine();
                    first_file_lines.Add(fline_first_file);
                    second_file_lines.Add(fline_second_file);

                    String line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        String[] split = line.Split(',');
                        if (split[8].Contains("USA"))
                        {
                            if (!second_file_values.ContainsKey(split[0]))
                            {
                                float[] val = new float[2];
                                val[0] = val[1] = 1000f;
                                float c_value = float.Parse(split[5].Substring(1));
                                if (c_value < val[0])
                                {
                                    val[1] = val[0];
                                    val[0] = c_value;
                                }
                                else if (c_value < val[1])
                                {
                                    val[1] = c_value;
                                }
                                second_file_values.Add(split[0], val);
                            }
                            else
                            {
                                float[] val = second_file_values[split[0]];
                                float c_value = float.Parse(split[5].Substring(1));
                                if (c_value < val[0])
                                {
                                    val[1] = val[0];
                                    val[0] = c_value;
                                    second_file_values[split[0]] = val;
                                }
                                else if (c_value < val[1])
                                {
                                    val[1] = c_value;
                                    second_file_values[split[0]] = val;
                                }
                            }
                            first_file_lines.Add(line);
                        }
                    }
                    reader.Close();
                }
                foreach (KeyValuePair<string, float[]> item in second_file_values)
                {
                    string line = item.Key + "," + item.Value[0].ToString() + "," + item.Value[1].ToString();
                    second_file_lines.Add(line);
                }
                using (StreamWriter writer = new StreamWriter(first_file))
                {
                    foreach (String line in first_file_lines)
                        writer.WriteLine(line);
                    writer.Close();
                }
                using (StreamWriter writer = new StreamWriter(second_file))
                {
                    foreach (String line in second_file_lines)
                        writer.WriteLine(line);
                    writer.Close();
                }
            }
        }
    }
}
