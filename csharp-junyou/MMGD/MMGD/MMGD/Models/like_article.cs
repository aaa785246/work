﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MMGD.Models;

[PrimaryKey("article_number", "email")]
public partial class like_article
{
    [Key]
    [StringLength(50)]
    public string email { get; set; }

    [Key]
    public int article_number { get; set; }
}