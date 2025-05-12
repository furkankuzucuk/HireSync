import React, { useEffect, useState } from 'react';
import { submitSurveyAnswers } from '../services/SurveyService';

interface SurveyAnswer {
  SurveyAnswerId: number;
  Answer: string;
}

interface SurveyQuestion {
  SurveyQuestionId: number;
  QuestionText: string;
  QuestionType: string; // 'MultipleChoice' or 'Text'
  SurveyAnswers: SurveyAnswer[];
}

interface SurveyFormProps {
  surveyId: number;
  questions: SurveyQuestion[];
}

const SurveyForm: React.FC<SurveyFormProps> = ({ surveyId, questions }) => {
  const [answers, setAnswers] = useState<Record<number, string>>({});

  useEffect(() => {
    const initialAnswers: Record<number, string> = {};
    questions.forEach((question) => {
      initialAnswers[question.SurveyQuestionId] = ''; // Initialize empty answers for each question
    });
    setAnswers(initialAnswers);
  }, [questions]);

  const handleAnswerChange = (questionId: number, answer: string) => {
    setAnswers((prevAnswers) => ({
      ...prevAnswers,
      [questionId]: answer, // Update answer for the specific questionId
    }));
  };

  const handleSubmit = async (event: React.FormEvent) => {
    event.preventDefault();

    const userIdStr = localStorage.getItem('userId');
    if (!userIdStr) {
      alert('User ID not found.');
      return;
    }

    const userId = Number(userIdStr);
    if (isNaN(userId)) {
      alert('Invalid User ID.');
      return;
    }

    const surveyData = {
      satisfactionSurveyId: surveyId, // Note lowercase 's' if required by backend
      answers: Object.entries(answers).map(([questionId, answer]) => ({
        questionId: Number(questionId), // Changed to 'questionId' here to match the service's expectation
        answer: answer, // Note lowercase 'a' if required
      })),
    };

    try {
      await submitSurveyAnswers(userId, surveyData); // Send the survey data
      alert('Survey submitted successfully!');
    } catch (error) {
      console.error('Submission error:', error);
      alert('Error submitting the survey.');
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      {questions.map((question) => (
        <div key={question.SurveyQuestionId} className="question">
          <label>{question.QuestionText}</label>
          <div>
            {question.QuestionType === 'MultipleChoice' ? (
              question.SurveyAnswers.map((answer) => (
                <div key={answer.SurveyAnswerId}>
                  <input
                    type="radio"
                    id={`question-${question.SurveyQuestionId}-answer-${answer.SurveyAnswerId}`}
                    name={`question-${question.SurveyQuestionId}`}
                    value={answer.Answer}
                    checked={answers[question.SurveyQuestionId] === answer.Answer}
                    onChange={() => handleAnswerChange(question.SurveyQuestionId, answer.Answer)}
                  />
                  <label htmlFor={`question-${question.SurveyQuestionId}-answer-${answer.SurveyAnswerId}`}>
                    {answer.Answer}
                  </label>
                </div>
              ))
            ) : (
              <input
                type="text"
                value={answers[question.SurveyQuestionId] || ''}
                onChange={(e) => handleAnswerChange(question.SurveyQuestionId, e.target.value)}
                placeholder="Your answer"
              />
            )}
          </div>
        </div>
      ))}
      <button type="submit">Submit Survey</button>
    </form>
  );
};

export default SurveyForm;
