using Application.Features.Category.Commands.CreateCategory;
using Application.Features.Category.Commands.DeleteCategory;
using Application.Features.Category.Commands.UpdateCategory;
using Application.Features.Category.Queries.GetAllCategories;
using Application.Features.Category.Queries.GetAllCategoriesDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
	private readonly IMediator _mediator;

    public CategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/<CategoriesController>
    [HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public async Task<IEnumerable<CategoryResponse>> GetAllCategories()
	{
		IEnumerable<CategoryResponse> categories = await _mediator.Send(new GetAllCategoriesQuery());
		return categories;
	}

	// GET api/<CategoriesController>/5
	[HttpGet("{id}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<CategoryDetailsResponse> GetCategoryById(int id)
	{
		CategoryDetailsResponse category = await _mediator.Send(new GetCategoryDetailsQuery { Id = id });
		return category;
	}

	// POST api/<CategoriesController>
	[HttpPost]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<ActionResult> CreateCategory(CreateCategoryCommand categoryToCreate)
	{
		int id = await _mediator.Send(categoryToCreate);
		return CreatedAtAction(nameof(GetAllCategories), new { id = id });
	}

	// PUT api/<CategoriesController>
	[HttpPut]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<ActionResult> UpdateCategory(UpdateCategoryCommand categoryToUpdate)
	{
		await _mediator.Send(categoryToUpdate);
		return NoContent();
	}

	// DELETE api/<CategoriesController>/5
	[HttpDelete("{id}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult> Delete(int id)
	{
		await _mediator.Send(new DeleteCategoryCommand { Id = id });
		return NoContent();
	}
}
