import React, { useEffect, useState } from "react";
import axios from "axios";
import "../css/WorkerAnnouncements.css";

interface Announcement {
  announcementId: number;
  title: string;
  content: string;
  createdDate: string;
}

const WorkerAnnouncements = () => {
  const [announcements, setAnnouncements] = useState<Announcement[]>([]);

  useEffect(() => {
    axios
      .get("/api/announcements")
      .then((res) => {
        setAnnouncements(res.data);
      })
      .catch(() => {
        console.error("Duyurular alınırken hata oluştu.");
      });
  }, []);

  return (
    <div className="worker-announcements">
      <h2>Duyurular</h2>
      <ul>
        {announcements.map((a) => (
          <li key={a.announcementId}>
            <strong>{a.title}</strong>
            <p>{a.content}</p>
            <p>{new Date(a.createdDate).toLocaleDateString("tr-TR")}</p>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default WorkerAnnouncements;
