using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductGenre> ProductGenres { get; set; }
        public DbSet<ProductProductGenre> ProductProductGenres { get; set; }
    }

    public class User : Microsoft.AspNetCore.Identity.IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Cart Cart {get; set;}

    }
    public class ProductType
    {
        public ProductType()
        {
            this.Products = new HashSet<Product>();
        }
        
        [Key]
        public string Name { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? LastModified { get; set; }
        public ICollection<Product> Products { get; set; }


    }

    public class ProductGenre
    {
        public ProductGenre()
        {
            this.ProductProductGenres = new HashSet<ProductProductGenre>();
        }
        [Key]
        public string Name { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? LastModified { get; set; }
        public ICollection<ProductProductGenre> ProductProductGenres { get; set; }

    }

    public class ProductProductGenre
    {
        public int ID { get; set; }
        public Product Product { get; set; }
        public ProductGenre ProductGenre { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? LastModified { get; set; }
    }


    public class Product
    {
        public Product()
        {
            this.CartItems = new HashSet<CartItem>();
            this.ProductProductGenres = new HashSet<ProductProductGenre>();
        }
        public int ID { get; set; }
        [Column(TypeName = "money")]
        public decimal? Price { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string TrailerUrl { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? LastModified { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
        public ProductType ProductType { get; set; }
        [ForeignKey("ProductType")]
        public string ProductTypeName { get; set; }
        public ICollection<ProductProductGenre> ProductProductGenres { get; set; }
    }

    public class Cart
    {
        public Cart()
        {
            this.CartItems = new HashSet<CartItem>();
        }
        public int ID { get; set; }
        public Guid? CookieID { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? LastModified { get; set; }
    }

    public class CartItem
    {
        public int ID { get; set; }
        public Cart Cart { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? LastModified { get; set; }

    }

    public class Order
    {
        public Order()
        {
            this.OrderItems = new HashSet<OrderItem>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid ID { get; set; }
        public string ContactEmail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ShippingStreet { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingState { get; set; }
        public string ShippingPostalCode { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? LastModified { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }

    }
    public class OrderItem
    {
        public int ID { get; set; }
        public int Quantity { get; set; }
        public int? ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        [Column(TypeName = "money")]
        public decimal? ProductPrice { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? LastModified { get; set; }
        public Order Order { get; set; }

    }
}
