export interface QuestionDto {
    questionId: number;
    examId: number;
    questionText: string;
    answerOptions: string[];
    correctAnswer: string;
}

export interface ExamDto {
    examId: number;
    examName: string;
    examDate: string;
}

export interface UserExamAnswerDto {
    examId: number;
    answers: {
        [questionId: number]: string;
    };
}
