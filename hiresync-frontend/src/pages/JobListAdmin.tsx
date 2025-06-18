import React, { useEffect, useState } from 'react';
import axios from 'axios';
import "../css/JobListAdmin.css";

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
    <div className="job-list-admin container py-4">
      <h2 className="text-center text-primary mb-4">📋 Yayınlanan İlanlar</h2>
      <div className="table-responsive">
        <table className="table table-striped table-bordered">
          <thead className="table-dark">
            <tr>
              <th>Departman</th>
              <th>Pozisyon</th>
              <th>Başlık</th>
              <th>Açıklama</th>
              <th>Oluşturulma Tarihi</th>
              <th>İşlem</th>
            </tr>
          </thead>
          <tbody>
            {jobs.map(job => (
              <tr key={job.jobListId}>
                <td>{job.departmentName}</td>
                <td>{job.jobName}</td>
                <td>{job.title}</td>
                <td>{job.description}</td>
                <td>
                  {new Date(job.createDate).toLocaleDateString("tr-TR", {
                    day: '2-digit',
                    month: 'long',
                    year: 'numeric'
                  })}
                </td>
                <td>
                  <button
                    className="btn btn-danger btn-sm"
                    onClick={() => handleDelete(job.jobListId)}
                  >
                    Sil
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default JobListAdmin;
