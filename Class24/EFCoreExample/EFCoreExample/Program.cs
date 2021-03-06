﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EFCoreExample.Data;

namespace EFCoreExample
{
    class Program
    {
        private const int GET_ALL_PRODUCTS = 1;
        private const int GET_PRODUCT_BY_ID = 2;
        private const int CREATE_PRODUCT = 3;
        private const int UPDATE_PRODUCT = 4;
        private const int DELETE_PRODUCT = 5;
        private const int QUIT = 6;

        static void Main(string[] args)
        {
            bool quit = false;

            while (!quit)
            {
                int menuOption = GetMenuOption();

                switch (menuOption)
                {
                    case GET_ALL_PRODUCTS:
                        GetAllProducts();
                        break;
                    case GET_PRODUCT_BY_ID:
                        GetProductById();
                        break;
                    case CREATE_PRODUCT:
                        CreateProduct();
                        break;
                    case UPDATE_PRODUCT:
                        UpdateProduct();
                        break;
                    case DELETE_PRODUCT:
                        DeleteProduct();
                        break;
                    case QUIT:
                        quit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option selected!");
                        break;
                }

                Console.WriteLine();
            }

            Console.WriteLine("Thanks for using the application!");
            Console.ReadKey();
        }

        private static void GetAllProducts()
        {
            Console.WriteLine();
            using (var context = new ProductContext())
            {
                foreach (var product in context.Products)
                {
                    Console.WriteLine($"Product Id: {product.Id}: {product.Name} - Quantity in stock: {product.QuantityInStock} - Price: {product.Price:c}");
                }
            }
        }

        private static void GetProductById()
        {
            int productId = -1;

            Console.WriteLine();
            Console.Write("Please enter the ID of the product: ");
            while (!Int32.TryParse(Console.ReadLine(), out productId))
            {
                Console.Write("Please enter a valid number: ");
            }

            using (var context = new ProductContext())
            {
                foreach (var product in context.Products.Where(x => x.Id == productId))
                {
                    Console.WriteLine($"Product Id: {product.Id}: {product.Name} - Quantity in stock: {product.QuantityInStock} - Price: {product.Price:c}");
                }
            }
        }

        private static void CreateProduct()
        {
            string name;
            decimal price;
            int quantityInStock;

            Console.WriteLine();
            Console.Write("Enter the name of the product: ");
            name = Console.ReadLine();

            Console.Write("Enter the price of the product: ");
            while (!Decimal.TryParse(Console.ReadLine(), out price))
            {
                Console.Write("Please enter a valid price: ");
            }

            Console.Write("Enter the quantity of the product: ");
            while (!Int32.TryParse(Console.ReadLine(), out quantityInStock))
            {
                Console.Write("Please enter a valid quantity: ");
            }

            Product newProduct = new Product(name, price, quantityInStock);

            using (var context = new ProductContext())
            {
                context.Products.Add(newProduct);
                context.SaveChanges();
            }
        }

        private static void UpdateProduct()
        {
            int id;
            decimal price;

            Console.WriteLine();
            Console.Write("Enter the ID of the product to update: ");
            while (!Int32.TryParse(Console.ReadLine(), out id))
            {
                Console.Write("Please enter a valid id: ");
            }

            Console.Write("Enter the new price of the product:");
            while (!Decimal.TryParse(Console.ReadLine(), out price))
            {
                Console.Write("Please enter a valid price: ");
            }

            using (var context = new ProductContext())
            {
                Product product = context.Products.FirstOrDefault(x => x.Id == id);

                if (product == null)
                {
                    Console.WriteLine("Product not found!");
                }
                else
                {
                    product.Price = price;
                }

                context.SaveChanges();
            }
        }

        private static void DeleteProduct()
        {
            int id;

            Console.WriteLine();
            Console.Write("Enter the ID of the product to delete: ");
            while (!Int32.TryParse(Console.ReadLine(), out id))
            {
                Console.Write("Please enter a valid id: ");
            }

            using (var context = new ProductContext())
            {
                Product product = context.Products.FirstOrDefault(x => x.Id == id);

                if (product == null)
                {
                    Console.WriteLine("Product not found!");
                }
                else
                {
                    context.Products.Remove(product);
                }

                context.SaveChanges();
            }
        }

        static int GetMenuOption()
        {
            int menuOption = -1;
            
            Console.WriteLine("Select one of the following options:");
            Console.WriteLine($"{GET_ALL_PRODUCTS} - Get all products");
            Console.WriteLine($"{GET_PRODUCT_BY_ID} - Get product by Id");
            Console.WriteLine($"{CREATE_PRODUCT} - Create a product");
            Console.WriteLine($"{UPDATE_PRODUCT} - Update a product");
            Console.WriteLine($"{DELETE_PRODUCT} - Delete a product");
            Console.WriteLine($"{QUIT} - Quit");

            Console.Write("Please enter your selection: ");
            while (!Int32.TryParse(Console.ReadLine(), out menuOption))
            {
                Console.Write("Please enter a valid number: ");
            }

            return menuOption;
        }
    }
}
