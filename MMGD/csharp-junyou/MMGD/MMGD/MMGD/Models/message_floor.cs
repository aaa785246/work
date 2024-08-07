﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MMGD.Models;

[PrimaryKey("username", "article_number", "msg_floor")]
public partial class message_floor
{
    [Key]
    [StringLength(20)]
    public string username { get; set; }

    [Key]
    [StringLength(10)]
    public string msg_floor { get; set; }

    [Key]
    public int article_number { get; set; }

    [Required]
    [StringLength(2000)]
    public string msg_content { get; set; }
}