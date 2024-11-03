using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TodoList.Models.Entities;

public class User : IdentityUser
{

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime BirthDate { get; set; }


    public string City { get; set; }

    public List<Post> Posts { get; set; }

    public List<Comment> Comments { get; set; }
}
