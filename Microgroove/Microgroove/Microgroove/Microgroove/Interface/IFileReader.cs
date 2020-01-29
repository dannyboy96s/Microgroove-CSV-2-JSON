using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microgroove.Models;
using System.IO;
using Newtonsoft.Json;

namespace Microgroove.Interface
{
    public interface IFileReader
    {
        bool WriteFile(string fileName, string contents);
        bool WriteFile<T>(string fileName, T contents);
        HashSet<MicroGrooveModel> ReadCsvData(string fileName);
    }

    public class FileReader : IFileReader
    {
        /// <summary>
        /// Parse the csv file containing orders and populate our Microgroove model in json format.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns> HashSet<MicroGrooveModel> </returns>
        public HashSet<MicroGrooveModel> ReadCsvData(string fileName)
        {
         
            var csv = new List<string[]>();

            // store each line as a string arr from the csv filw
            var lines = File.ReadAllLines(fileName);

            // for each line in line, split by delimiter ',' and add each string to the csv list
            foreach (string line in lines)
            {
                csv.Add(line.Split(','));
            }

            
            HashSet<MicroGrooveModel> toJson = new HashSet<MicroGrooveModel>();

            //Console.WriteLine(JsonConvert.SerializeObject(csv));
            
            MicroGrooveModel currentMGM = null;
            Order currentOrder = null;

            foreach (string[] lineTokens in csv)
            {
                // create a new entry when we see F
                if (lineTokens[0].Equals("F"))
                {
                    currentMGM = new MicroGrooveModel();
                    currentMGM.Date = lineTokens[1];
                    currentMGM.Type = lineTokens[2];
                    currentMGM.Orders = new List<Order>();

                    // add an order entry to our set
                    toJson.Add(currentMGM);

                }
                else if (lineTokens[0].Equals("O"))
                {
                    // if we hit 'O' and currentMGM is null, then thats an error - throw some exception
                    if (currentMGM == null)
                    {
                        throw new Exception("Empty MicroGrooveModel");
                    }
                    currentOrder = new Order();
                    currentOrder.OrderDate = lineTokens[1];
                    currentOrder.Code = lineTokens[2];
                    currentOrder.Number = lineTokens[3];
                    currentOrder.Items = new List<Item>();

                    // adding the current order to currentMGM in whatever state its in
                    currentMGM.Orders.Add(currentOrder);


                }
                else if (lineTokens[0].Equals("B"))
                {                    
                    currentOrder.Buyer = new Buyer();
                    currentOrder.Buyer.Name = lineTokens[1];
                    currentOrder.Buyer.Street = lineTokens[2];
                    currentOrder.Buyer.Zip = lineTokens[3];

                }
                else if (lineTokens[0].Equals("L"))
                {
                    Item item = new Item();
                    item.Sku = lineTokens[1];
                    item.Quantity = Convert.ToInt32(lineTokens[2]);
                    currentOrder.Items.Add(item);

                }
                else if (lineTokens[0].Equals("T"))
                {
                    currentOrder.Timing = new Timing();
                    currentOrder.Timing.Start = Convert.ToInt32(lineTokens[1]);
                    currentOrder.Timing.Stop = Convert.ToInt32(lineTokens[2]);
                    currentOrder.Timing.Gap = Convert.ToInt32(lineTokens[3]);
                    currentOrder.Timing.Offset = Convert.ToInt32(lineTokens[4]);
                    currentOrder.Timing.Pause = Convert.ToInt32(lineTokens[5]);

                }
                else if (lineTokens[0].Equals("E"))
                {
                    currentMGM.Ender = new Ender();
                    currentMGM.Ender.Process = Convert.ToInt32(lineTokens[1]);
                    currentMGM.Ender.Paid = Convert.ToInt32(lineTokens[2]);
                    currentMGM.Ender.Created = Convert.ToInt32(lineTokens[3]);

                }
            }

            return toJson;

        }

        /// <summary>
        /// Take the output file path and contents and write to that output file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="contents"></param>
        /// <returns>bool</returns>
        public bool WriteFile(string fileName, string contents)
        {
            try
            {
                File.WriteAllText(fileName, contents);
                return true;
            } catch
            {
                return false;
            }
        }

        /// <summary>
        ///  Take the output file path and T contents (in this case our HashSet<MicroGrooveModel> ) and write to that output file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <param name="contents"></param>
        /// <returns>bool</returns>
        public bool WriteFile<T>(string fileName, T contents)
        {
            try
            {
                string toJson = JsonConvert.SerializeObject(contents, Formatting.Indented);
                WriteFile(fileName, toJson);
                return true;
            } catch
            {
                return false;
            }
        }
    }
}
