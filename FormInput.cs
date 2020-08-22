using System;
using System.Reflection;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace DataProcessor
{
    public class Transformer
    {
        // used to store finalized data before turning into List<ProductRecord>
        Dictionary<string, List<object>> finalized_data = new Dictionary<string, List<object>>();

        //public Transformer(List<Dictionary<string, string>> input_data, List<ProductRecord> schema)
        //{
        //    this.initial_data = data;
        //}

        public List<ProductRecord> TransToProductRecord(Dictionary<string, List<string>> initial_data)
        {
            SetFinalOutput(initial_data);
            List<ProductRecord> records = new List<ProductRecord>();

            for (int i = 0; i < this.finalized_data["product_id"].Count; i++)
            {
                ProductRecord pr = new ProductRecord();
                foreach (KeyValuePair<string, List<object>> item in this.finalized_data)
                {
                    Console.WriteLine(item.Key, item.Value);
                    PropertyInfo propertyInfo = pr.GetType().GetProperty(item.Key);
                    propertyInfo.SetValue(pr, Convert.ChangeType(item.Value[i], propertyInfo.PropertyType), null);
                }
                records.Add(pr);
            }
            return records;
        }

        private void SetFinalOutput(Dictionary<string, List<string>> initial_data)
        {
            //Console.WriteLine("sdfdsf");
            //Console.WriteLine(this.finalized_data["product_id"][0].GetType());
            //Cosnole.WriteLine(initial_data["product_id"][0].GetType());
            this.finalized_data["product_id"] =             initial_data["product_id"].Select(x => (object)x).ToList();
            this.finalized_data["product_description"] =    initial_data["product_description"].Select(x => (object)x).ToList(); ;

            this.finalized_data["unit_of_measure"] =        TransUnitOfMeasurement(initial_data["flags"]);
            this.finalized_data["product_size"] =           initial_data["product_size"].Select(x => (object)x).ToList(); ;
            this.finalized_data["tax_rate"] =               TransTaxRate(initial_data["flags"]);
        }

        // Individual Transformations per Field
        //private List<object> TransformRegularPrice(price_list)
        //{
        //    List<object> output_list = price_list;
        //    output_list.ForEach(i => {
        //        i = 

        //    });

        //}

        private List<object> TransUnitOfMeasurement(List<string> data)
        {
            List<object> output_list = new List<object>();
            foreach (string item in data)
            {
                output_list.Add(item[2] == 'Y' ? "Pound" : "Each");
            }
            return output_list;
        }


        private List<object> TransTaxRate(List<string> data)
        {
            List<object> output_list = new List<object>();
            foreach (string item in data)
            {
                output_list.Add(item[4] == 'Y' ? 0.07775 : 0);
            }
            return output_list;
        }
    }
}