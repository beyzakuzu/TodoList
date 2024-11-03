using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Modelss.Entities;

public enum Priority
{
    Low,
    Normal,
    High
}

public class Todo : Entity<Guid>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Priority Priority { get; set; }  
    public int CategoryId { get; set; }
    public bool Completed { get; set; }
    public Category Category { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
}