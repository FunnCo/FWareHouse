using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWareHouse.common.entity
{
    public class Employee
    {
        [Key]
        public int id { get; set; }
        public string firstname { get; set; }
        public string surname { get; set; }
        public string patronymic { get; set; }
        public string phone { get; set; }
        public string address { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime date_of_birth { get; set; }
        public int position_id { get; set; }
        public string passport { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        public Employee(int id, string firstname, string surname, string patronymic, string phone, string address, DateTime date_of_birth, int position_id, string passport, string email, string password)
        {
            this.id = id;
            this.firstname = firstname;
            this.surname = surname;
            this.patronymic = patronymic;
            this.phone = phone;
            this.address = address;
            this.date_of_birth = date_of_birth;
            this.position_id = position_id;
            this.passport = passport;
            this.email = email;
            this.password = password;
        }
    }
}
