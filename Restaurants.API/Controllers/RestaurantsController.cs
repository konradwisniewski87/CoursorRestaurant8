using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.DTOs;
using Restaurants.Application.Services;

namespace Restaurants.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RestaurantsController : ControllerBase
{
    private readonly IRestaurantsService _restaurantsService;

    public RestaurantsController(IRestaurantsService restaurantsService)
    {
        _restaurantsService = restaurantsService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll()
    {
        var restaurants = await _restaurantsService.GetAllAsync();
        return Ok(restaurants);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RestaurantDto>> GetById(int id)
    {
        var restaurant = await _restaurantsService.GetByIdAsync(id);
        if (restaurant == null)
        {
            return NotFound();
        }
        return Ok(restaurant);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] CreateRestaurantDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var id = await _restaurantsService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while creating the restaurant", details = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] CreateRestaurantDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _restaurantsService.UpdateAsync(id, dto);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while updating the restaurant", details = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await _restaurantsService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while deleting the restaurant", details = ex.Message });
        }
    }
} 