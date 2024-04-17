using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{	public class DProducts
	{

		public int Id { get; set; }
		public string? ProductName { get; set; }

		public string? ProductDescription { get; set; }

		public string? ProductCategory { get; set; }

		public decimal ProductPrice { get; set; }

		public int Stock { get; set; }

		public string? Img { get; set; }

        public int? ItemOrderQuatity { get; set; }
    }
}
