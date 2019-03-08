using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;

namespace WpfClient
{

    public class Equipment
    {
        public int IdEquipment { get; set; }
        public int IdBrand { get; set; }
        public int IdToolType { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }
    static class WebApiRequestClass
    {
        static HttpClient client = new HttpClient();
        /// <summary>
		///     Возвращает список задач для данного пользователя
		/// </summary>
		/// <returns></returns>
		static public List<Equipment> EquipmentList()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:41838/");
            //client.DefaultRequestHeaders.Add("appkey", "myapp_key");
            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("api/EquipmentsWebApi").Result;
            if (response.IsSuccessStatusCode)
            {
                var equipments = response.Content.ReadAsAsync<String>().Result;
               // var eqStr = equipments.Replace('[', ' ');
                dynamic res = JsonConvert.DeserializeObject(equipments);
                List<Equipment> organizations = res.ToObject<List<Equipment>>();
                return organizations;

                
            }
            else
            {
                MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
                return new List<Equipment>();
            }
            
        }


    }
}
