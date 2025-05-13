export interface SurveySubmissionDto {
    answers: {
        surveyQuestionId: number;
        answerText: string;
    }[];
}
