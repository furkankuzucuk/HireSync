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
