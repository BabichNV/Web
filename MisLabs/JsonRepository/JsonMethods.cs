using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace JsonRepository
{
    public class JsonMethods
    {  
        string path = Environment.CurrentDirectory+ @"\textjson.json";
        public void WriteToFile(List<Account> bills)
        {
            bills = bills.OrderBy(a => a.Date).ToList();
            string s = JsonConvert.SerializeObject(bills);
            using (StreamWriter str = new StreamWriter(path, false, System.Text.Encoding.Default))
            {
                str.Write(s);
            }
        }

        public List<Account> GetAccounts()
        {
            using (var file = File.OpenRead(path))
            {
                byte[] array = new byte[file.Length];
                file.Read(array, 0, array.Length);
                string json = System.Text.Encoding.Default.GetString(array);
                return JsonConvert.DeserializeObject<List<Account>>(json, new IsoDateTimeConverter { DateTimeFormat = "dd.MM.yyyy" });
            }
        }

        public Account GetAccount(Guid id)
        {
            return GetAccounts().Find(ac => ac.Id == id);
        }

        public void AddAccount(Account account)
        {
            List<Account> bills = GetAccounts();
            account.Id = Guid.NewGuid();
            bills.Add(account);
            WriteToFile(bills);
        }

        public void RemoveAccount(Guid id)
        {
            List<Account> bills = GetAccounts();
            bills.Remove(bills.Find(a => a.Id == id));
            WriteToFile(bills);
        }

        public List<Account> GetAccountsBetweenDates(DateTime leftDate, DateTime rightDate)
        {
            List<Account> bills = GetAccounts();
            bills.RemoveAll(a => a.Date.Date < leftDate.Date || a.Date.Date > rightDate.Date);
            return bills;
        }

        public void UpdateAccount (Account account)
        {
            List<Account> bills = GetAccounts();
            bills.RemoveAll(a => a.Id == account.Id);
            bills.Add(account);
            WriteToFile(bills);
        }
    }
}
