using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class QuizDataController
{
   private static string ROW_SEPARATOR = @"\r\n|\n\r|\n|\r";
   static string COL_SEPARATOR = @"\s*, \s*";
   public static List<QuizData> LoadQuizData(int stageIndex)
   {
      var fileName = "QuizData-" + stageIndex;
      
      TextAsset quizDataAsset = Resources.Load("QuizData") as TextAsset;
      var lines = Regex.Split(quizDataAsset.text, ROW_SEPARATOR);

      var quizDataList = new List<QuizData>();
      
      for (var i = 0; i < lines.Length; i++)
      {
         var values = Regex.Split(lines[i], COL_SEPARATOR);
         
         QuizData quizData = new QuizData();
         
         quizData.question = values[0];
         quizData.description = values[1];
         quizData.type = int.Parse(values[2]);
         quizData.answers = int.Parse(values[3]);
         
         string[] options = { values[4], values[5], values[6]};
         quizData.options = options;
         
         quizDataList.Add(quizData);
      }

      return quizDataList;
   }
}
