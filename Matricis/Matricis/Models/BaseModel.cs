using Matricis.Helpers;
using SQLite;
using System;

namespace Matricis.Models {
    public class BaseModel : ObservableObject {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public String Title { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}