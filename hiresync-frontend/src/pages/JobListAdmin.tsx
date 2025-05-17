// src/pages/JobListAdmin.tsx
import React, { useEffect, useState } from 'react';
import axios from 'axios';

interface JobListDto {
  jobListId: number;
  departmentName: string;
  jobName: string;
  title: string;
  description: string;
  createDate: string;
}

const JobListAdmin = () => {
  const [jobs, setJobs] = useState<JobListDto[]>([]);

  useEffect(() => {
    axios.get('/api/joblists')
      .then(res => setJobs(res.data))
      .catch(err => console.error(err));
  }, []);

  const handleDelete = (id: number) => {
    axios.delete(`/api/joblists/${id}`)
      .then(() => setJobs(prev => prev.filter(job => job.jobListId !== id)))
      .catch(err => console.error(err));
  };

  return (
    <div>
      <h2>İş İlanları (Admin)</h2>
      <table className="table">
        <thead>
          <tr>
            <th>Departman</th>
            <th>İş</th>
            <th>Başlık</th>
            <th>Açıklama</th>
            <th>Oluşturulma Tarihi</th>
            <th>İşlemler</th>
          </tr>
        </thead>
        <tbody>
          {jobs.map(job => (
            <tr key={job.jobListId}>
              <td>{job.departmentName}</td>
              <td>{job.jobName}</td>
              <td>{job.title}</td>
              <td>{job.description}</td>
              <td>{new Date(job.createDate).toLocaleDateString()}</td>
              <td>
                {/* Update için yönlendirme olabilir */}
                <button className="btn btn-danger btn-sm" onClick={() => handleDelete(job.jobListId)}>Sil</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default JobListAdmin;
