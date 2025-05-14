export interface SurveyAnswerInsertDto {
  surveyQuestionId: number;
  answer: string; // ✅ Doğru alan adı
}

export interface SurveySubmissionDto {
  satisfactionSurveyId: number;
  answers: SurveyAnswerInsertDto[];
}

export interface SurveyAnswerSummary {
  satisfactionSurveyId: number;
  surveyTitle: string;
}

export interface SurveyQuestion {
  surveyQuestionId: number;
  questionText: string;
  questionType: string;
}