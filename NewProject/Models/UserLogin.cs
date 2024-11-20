using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NewProject.Models;

public partial class UserLogin
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // Đảm bảo UserId tự động tăng

    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? FullName { get; set; }

    public string? PhoneNumber { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? LastLoginDate { get; set; }

    public bool? IsActive { get; set; }

    public int? FailedLoginAttempts { get; set; }

    public DateTime? LockedOutUntil { get; set; }
}
