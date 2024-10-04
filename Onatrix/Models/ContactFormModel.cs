namespace Onatrix.Models;

public class ContactFormModel
{
	public string? UserName { get; set; }
	public string Email { get; set; } = null!;
	public string? Message { get; set; }
	public string? Phone { get; set; }
}
