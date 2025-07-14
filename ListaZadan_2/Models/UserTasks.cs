using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaZadan_2.Models
{
    public class UserTasks
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int UserId { get; set; }
        public string TaskName { get; set; } = "Zadanie";
        public string Description { get; set; }= string.Empty;
        public DateTime DueDate { get; set; }= DateTime.Now;

        public bool IsCompleted { get; set; }= false;

        public UserTasks Clone() => MemberwiseClone() as UserTasks;

        public (bool IsValid, string? ErrorMessage) Validate() { 
              if(string.IsNullOrWhiteSpace(TaskName))
                return (false, "Nazwa zadania nie może być pusta.");
              return (true, null);
        }
    }
}
