using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Models.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Todo> ToDos { get; set; }
}

