﻿using Quiz.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Core.ViewModels
{
    public class QuizThemesViewModel
    {
        public Quizz Quiz { get; set; }
        public List<Theme> Themes { get; set; }
    }
}
