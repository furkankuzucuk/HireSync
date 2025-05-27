import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import axios from 'axios';
import "../css/JobEdit.css";

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

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    axios.put(`/api/joblists/${id}`, form)
      .then(() => navigate('/admin-dashboard/jobs'))
      .catch(err => console.error(err));
  };

  return (
    <div className="job-edit container py-5">
      <div className="card shadow p-4">
        <h2 className="mb-4 text-center text-primary">📄 İlan Güncelle</h2>
        <form onSubmit={handleSubmit}>
          <div className="mb-3">
            <label className="form-label">İlan Başlığı</label>
            <input
              name="jobTitle"
              value={form.jobTitle}
              onChange={handleChange}
              placeholder="İlan Başlığı"
              className="form-control"
              required
            />
          </div>
          <div className="mb-3">
            <label className="form-label">Açıklama</label>
            <textarea
              name="description"
              value={form.description}
              onChange={handleChange}
              placeholder="Açıklama"
              className="form-control"
              rows={4}
              required
            />
          </div>
          <button type="submit" className="btn btn-success w-100">Kaydet</button>
        </form>
      </div>
    </div>
  );
};

export default JobEdit;
