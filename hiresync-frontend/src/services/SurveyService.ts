export const getSurveysForUser = async () => {
  const token = localStorage.getItem('token');

  const response = await fetch('/api/satisfactionsurveys/user-department', {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`,
    },
  });

  if (!response.ok) {
    throw new Error(`Survey fetch failed with status ${response.status}`);
  }

  return await response.json();
};

export const getSurveyById = async (id: number) => {
  const token = localStorage.getItem('token');

  const response = await fetch(`/api/satisfactionsurveys/${id}`, {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`,
    },
  });

  if (!response.ok) {
    throw new Error(`Survey fetch by ID failed with status ${response.status}`);
  }

  return await response.json();
};
