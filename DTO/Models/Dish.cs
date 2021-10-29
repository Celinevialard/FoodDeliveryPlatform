using System;

namespace DTO
{
  public class Dish
  {
    public int DishId { get; set; }

    public int RestaurantId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Allergies { get; set; }

    public decimal Price { get; set; }

    public string ImageLink { get; set; }


  }
}
