using System;
using System.Collections.Generic;
using System.Text;

namespace MA.Data.Model
{
    public class TaskMessage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FirstSend { get; set; }
        public string LastSent { get; set; }
        public string ApiType { get; set; }
        public string ApiParam { get; set; }
        public int UserId { get; set; }
        public int Pereodicity { get; set; }

    }
}
