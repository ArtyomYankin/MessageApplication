namespace MA.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class UserWithTasks
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string ApiType { get; set; }
        public string ApiParam { get; set; }
        public string FirstSend { get; set; }
        public int Pereodicity { get; set; }
    }
}
