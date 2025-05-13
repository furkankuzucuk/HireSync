import { SurveySubmissionDto } from '../types/surveyTypes';

export const submitSurveyAnswers = async (submission: SurveySubmissionDto) => {
  const token = localStorage.getItem('token');

  const response = await fetch('/api/surveyanswers/submit', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`,
    },
    body: JSON.stringify(submission),
  });

  if (!response.ok) {
    throw new Error(`Answer submission failed with status ${response.status}`);
  }

  return await response.json();
};

export const getSubmittedSurveys = async () => {
  const token = localStorage.getItem('token');

  const response = await fetch('/api/surveyanswers', {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`,
    },
  });

  if (!response.ok) {
    throw new Error(`Submitted surveys fetch failed with status ${response.status}`);
  }

  return await response.json();
};
