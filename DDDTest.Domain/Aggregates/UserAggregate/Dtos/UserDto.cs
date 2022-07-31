using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDTest.Domain.Aggregates.UserAggregate.Dtos {
    public class UserDto {
        public string UserName { get; set; }
        public string Password { get; set; }
        public byte[] Avatar { get; set; }

    }
}
