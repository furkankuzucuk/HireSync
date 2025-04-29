import React, { useState, useEffect } from "react";
import axios from "axios";
import { Link } from "react-router-dom";
import "../css/LandingPage.css";

const LandingPage = () => {
  const [jobListings, setJobListings] = useState<any[]>([]); // İş ilanlarını tutacak state

  useEffect(() => {
    // İş ilanlarını alacağız
    const fetchJobListings = async () => {
      try {
        const response = await axios.get("http://localhost:5065/api/joblists"); // Backend'den veri alıyoruz
        setJobListings(response.data); // Alınan veriyi state'e kaydediyoruz
      } catch (error) {
        console.error("İş ilanları alınırken hata oluştu", error);
      }
    };
    fetchJobListings(); // Sayfa yüklenince iş ilanlarını al
  }, []);

  return (
    <div className="container-fluid p-0">
      {/* Hero Section */}
      <section className="hero text-white text-center py-5">
        <div className="container">
          <h1 className="display-4 fw-bold">HireSync - İnsan Kaynakları</h1>
          <p className="lead">Profesyonel iş ilanları, kolay başvurular ve daha fazlası!</p>
          <Link to="/login" className="btn btn-light btn-lg">Giriş Yap</Link>
        </div>
      </section>

      {/* Featured Job Listings Section */}
      <section className="job-listings py-5">
        <div className="container">
          <h2 className="text-center mb-4">Güncel İş İlanları</h2>
          <div className="row">
            {/* Dinamik iş ilanları */}
            {jobListings.length > 0 ? (
              jobListings.map((job: any) => (
                <div className="col-md-4 mb-4" key={job.jobListId}>
                  <div className="card">
                    <div className="card-body">
                      <h5 className="card-title">{job.jobName}</h5>
                      <p className="card-text">{job.description}</p>
                      <Link to={`/candidate-dashboard/jobs`} className="btn btn-primary">İlanı Görüntüle</Link>
                    </div>
                  </div>
                </div>
              ))
            ) : (
              <p className="text-center">Henüz iş ilanı bulunmamaktadır.</p>
            )}
          </div>
        </div>
      </section>
    </div>
  );
};

export default LandingPage;
