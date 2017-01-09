using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go2MusicStore.Common
{
    using System.Runtime.CompilerServices;

    using Go2MusicStore.Models;

    public static class ExtenstionMethodHelper
    {
        public static IEnumerable<Album> CalculateTotalStarRating(this IEnumerable<Album> albums)
        {
            foreach (var album in albums)
            {
                var totalStarRatingSum = 0;
                if (album.Reviews.Any())
                {
                    totalStarRatingSum = album.Reviews.Sum(m => m.StarRating) / album.Reviews.Count;
                    album.TotalStarRating = Math.Abs(totalStarRatingSum);
                    if (album.TotalStarRating > 5)
                    {
                        album.TotalStarRating = 5;
                    }
                }
            }

            return albums;
        }

        public static Album CalculateTotalStarRating(this Album album)
        {        
            var totalStarRatingSum = 0;
            if (album.Reviews.Any())
            {
                totalStarRatingSum = album.Reviews.Sum(m => m.StarRating) / album.Reviews.Count;
                album.TotalStarRating = Math.Abs(totalStarRatingSum);
                if (album.TotalStarRating > 5)
                {
                    album.TotalStarRating = 5;
                }
            }
         
            return album;
        }

        public static IEnumerable<PurchaseOrder> CalculateTotalOrderAmount(this IEnumerable<PurchaseOrder> purchaseOrders)
        {
            foreach (var purchaseOrder in purchaseOrders)
            {
                var totalOrderAmount = 0.00;
                if (purchaseOrder.PurchaseOrderItems.Any())
                {
                    totalOrderAmount = purchaseOrder.PurchaseOrderItems.Sum(m => m.TotalAmount);
                    purchaseOrder.TotalOrderAmount = Math.Abs(totalOrderAmount);                    
                }
            }

            return purchaseOrders;
        }

        public static PurchaseOrder CalculateTotalOrderAmount(this PurchaseOrder purchaseOrder)
        {
            
            var totalOrderAmount = 0.00;
            if (purchaseOrder.PurchaseOrderItems.Any())
            {
                totalOrderAmount = purchaseOrder.PurchaseOrderItems.Sum(m => m.TotalAmount);
                purchaseOrder.TotalOrderAmount = Math.Abs(totalOrderAmount);
            }
            

            return purchaseOrder;
        }

        public static IEnumerable<PurchaseOrderItem> CalculateTotalOrderItemAmount(
            this IEnumerable<PurchaseOrderItem> purchaseOrderItems)
        {
            foreach (var purchaseOrderItem in purchaseOrderItems)
            {
                var totalOrderItemAmount = 0.00;
                if (purchaseOrderItem.Album != null)
                {
                    totalOrderItemAmount = purchaseOrderItem.Quantity * purchaseOrderItem.Album.Price;
                    purchaseOrderItem.TotalAmount = Math.Abs(totalOrderItemAmount);
                }
            }

            return purchaseOrderItems;
        }

        public static PurchaseOrderItem CalculateTotalOrderItemAmount(
            this PurchaseOrderItem purchaseOrderItem)
        {            
                var totalOrderItemAmount = 0.00;
                if (purchaseOrderItem.Album != null)
                {                    
                    totalOrderItemAmount = purchaseOrderItem.Quantity * purchaseOrderItem.Album.Price;
                    purchaseOrderItem.TotalAmount = Math.Abs(totalOrderItemAmount);
                }
           
            return purchaseOrderItem;
        }
    }
}
