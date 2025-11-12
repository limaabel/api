using EngineApplication.Domain.DTOs.Activity;
using EngineApplication.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIApplication.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ActivitiesController : ControllerBase
{
	private readonly IActivityRepository _activityRepository;
	private readonly ICustomerRepository _customerRepository;

	public ActivitiesController(
		IActivityRepository activityRepository,
		ICustomerRepository customerRepository)
	{
		_activityRepository = activityRepository;
		_customerRepository = customerRepository;
	}

	// ==============================
	// GET: api/activities
	// ==============================
	[HttpGet]
	public async Task<ActionResult<IEnumerable<ActivityDetailsDto>>> GetAllAsync()
	{
		var activities = await _activityRepository.GetAllAsync();
		if (activities == null || !activities.Any())
			return NoContent();

		var result = activities.Select(ActivityDetailsDto.FromEntity);
		return Ok(result);
	}

	// ==============================
	// GET: api/activities/{id}
	// ==============================
	[HttpGet("{id:int}", Name = "GetActivityById")]
	public async Task<ActionResult<ActivityDetailsDto>> GetByIdAsync(int id)
	{
		if (id <= 0)
			return BadRequest("Invalid activity ID.");

		var activity = await _activityRepository.GetByIdAsync(id);
		if (activity == null)
			return NotFound($"Activity with ID {id} not found.");

		return Ok(ActivityDetailsDto.FromEntity(activity));
	}

	// ==============================
	// POST: api/activities
	// ==============================
	[HttpPost]
	public async Task<ActionResult<ActivityDetailsDto>> CreateAsync([FromBody] ActivityCreateDto dto)
	{
		if (dto == null)
			return BadRequest("Activity data is required.");

		var customer = await _customerRepository.GetByIdAsync(dto.CustomerId);
		if (customer == null)
			return NotFound($"Customer with ID {dto.CustomerId} not found.");

		var activity = dto.ToEntity();
		await _activityRepository.AddAsync(activity);

		var result = ActivityDetailsDto.FromEntity(activity);
		return CreatedAtRoute("GetActivityById", new { id = result.ActivityId }, result);
	}

	// ==============================
	// PUT: api/activities/{id}
	// ==============================
	[HttpPut("{id:int}")]
	public async Task<ActionResult> UpdateAsync(int id, [FromBody] ActivityUpdateDto dto)
	{
		if (dto == null || id != dto.ActivityId)
			return BadRequest("Activity ID mismatch.");

		var existing = await _activityRepository.GetByIdAsync(id);
		if (existing == null)
			return NotFound($"Activity with ID {id} not found.");

		// Update fields that exist in the entity
		existing.ActivityType = dto.ActivityType;
		existing.Subject = dto.Subject;
		existing.Notes = dto.Notes;
		existing.PerformedBy = dto.PerformedBy;
		existing.CustomerId = dto.CustomerId;
		existing.CreatedAt = dto.CreatedAt;

		await _activityRepository.UpdateAsync(existing);
		return NoContent();
	}

	// ==============================
	// DELETE: api/activities/{id}
	// ==============================
	[HttpDelete("{id:int}")]
	public async Task<ActionResult> DeleteAsync(int id)
	{
		if (id <= 0)
			return BadRequest("Invalid activity ID.");

		var existing = await _activityRepository.GetByIdAsync(id);
		if (existing == null)
			return NotFound($"Activity with ID {id} not found.");

		await _activityRepository.DeleteAsync(id);
		return NoContent();
	}
}
