using System;
using System.Reflection;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace DataProcessor
{
    public class Transformer
    {
        // Used to store finalized data before turning into List<ProductRecord>
        Dictionary<string, List<object>> FinalizedData = new Dictionary<string, List<object>>();

        public List<ProductRecord> TransToProductRecord(Dictionary<string, List<string>> initialData)
        {
            SetFinalOutput(initialData);
            List<ProductRecord> records = new List<ProductRecord>();

            for (int i = 0; i < this.FinalizedData["productId"].Count; i++)
            {
                ProductRecord pr = new ProductRecord();
                foreach (KeyValuePair<string, List<object>> item in this.FinalizedData)
                {
                    PropertyInfo propertyInfo = pr.GetType().GetProperty(item.Key);
                    propertyInfo.SetValue(pr, Convert.ChangeType(item.Value[i], propertyInfo.PropertyType), null);
                }
                records.Add(pr);
            }
            return records;
        }

        private void SetFinalOutput(Dictionary<string, List<string>> initialData)
        {
            this.FinalizedData["productId"] =             initialData["productId"].Select(x => (object)x).ToList();
            this.FinalizedData["productDescription"] =    initialData["productDescription"].Select(x => (object)x).ToList(); ;

            this.FinalizedData["unitOfMeasure"] =         TransUnitOfMeasurement(initialData["flags"]);
            this.FinalizedData["productSize"] =           initialData["productSize"].Select(x => (object)x).ToList(); ;
            this.FinalizedData["taxRate"] =               TransTaxRate(initialData["flags"]);
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
            List<object> outputList = new List<object>();
            foreach (string item in data)
            {
                outputList.Add(item[2] == 'Y' ? "Pound" : "Each");
            }
            return outputList;
        }


        private List<object> TransTaxRate(List<string> data)
        {
            List<object> outputList = new List<object>();
            foreach (string item in data)
            {
                outputList.Add(item[4] == 'Y' ? 0.07775 : 0);
            }
            return outputList;
        }
    }
}