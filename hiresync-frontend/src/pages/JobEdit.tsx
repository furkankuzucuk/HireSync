import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import axios from 'axios';

const JobEdit = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [form, setForm] = useState({
    jobTitle: '',
    description: '',
    departmentId: 0,
    jobId: 0
  });

  useEffect(() => {
    axios.get(`/api/joblists/${id}`)
      .then(res => setForm(res.data))
      .catch(err => console.error(err));
  }, [id]);

  const handleChange = (e: any) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = (e: any) => {
    e.preventDefault();
    axios.put(`/api/joblists/${id}`, form)
      .then(() => navigate('/admin-dashboard/jobs'))
      .catch(err => console.error(err));
  };

  return (
    <div>
      <h2>İlan Güncelle</h2>
      <form onSubmit={handleSubmit}>
        <input name="jobTitle" value={form.jobTitle} onChange={handleChange} placeholder="İlan Başlığı" />
        <textarea name="description" value={form.description} onChange={handleChange} placeholder="Açıklama" />
        <button type="submit">Kaydet</button>
      </form>
    </div>
  );
};

export default JobEdit;
