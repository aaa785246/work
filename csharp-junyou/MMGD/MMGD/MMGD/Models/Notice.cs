﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MMGD.Models;

[PrimaryKey("email", "message_username", "airicle_number", "msg_floor")]
public partial class notice
{
    [Key]
    [StringLength(10)]
    public string email { get; set; }

    [Key]
    [StringLength(10)]
    public string message_username { get; set; }

    [Key]
    [StringLength(8)]
    public string airicle_number { get; set; }

    [Required]
    [StringLength(2000)]
    public string message_content { get; set; }

    [Required]
    [StringLength(1)]
    public string viewed { get; set; }

    [Key]
    [StringLength(10)]
    public string msg_floor { get; set; }
}