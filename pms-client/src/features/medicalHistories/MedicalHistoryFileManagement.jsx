import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useParams } from 'react-router-dom';
import FileInput from '../../ui/FileInput';
import Button from '../../ui/Button';
import Spinner from '../../ui/Spinner';


function MedicalHistoryFileManagement() {
  const [photos, setPhotos] = useState([]);
  const [file, setFile] = useState(null); // State to hold the uploaded file
  const [uploading, setUploading] = useState(false);
  const { userId } = useParams();

  useEffect(() => {
    async function fetchPhotos() {
      try {
        const response = await axios.get(`http://localhost:5000/api/photo/user/cc792d13-3681-494f-934a-15e34524454f`);
        if (response.status === 200) {
          setPhotos(response.data.$values);
        } else {
          throw new Error('Failed to fetch with status: ' + response.status);
        }
      } catch (error) {
        console.error("Failed to fetch photos:", error);
      }
    }

    fetchPhotos();
  }, []);

  const handleFileChange = (event) => {
    setFile(event.target.files[0]); // Set the file to the file chosen by the file input
  };

  const handleUpload = async () => {
    if (!file) return; // If no file is selected, return early

    const formData = new FormData();
    formData.append('File', file);
    formData.append('UserId', 'cc792d13-3681-494f-934a-15e34524454f'); // Assume a fixed user ID or manage via state/context

    setUploading(true);

    try {
      const response = await axios.post('http://localhost:5000/api/photo/', formData, {
        headers: {
          'Content-Type': 'multipart/form-data'
        }
      });

      setUploading(false);

      if (response.data.success) {
        setPhotos(prevPhotos => [...prevPhotos, response.data.photo]); // Assuming the API returns the added photo
      } else {
        throw new Error('Upload failed');
      }
    } catch (error) {
      console.error("Error uploading file:", error);
      setUploading(false);
    }
  };

  return (
    <div>
      <h2>User Photos</h2>
      <FileInput id="image" accept="image/*" type="file" onChange={handleFileChange} />
      <Button onClick={handleUpload} disabled={uploading}>
        {uploading ? 'Uploading' : 'Upload Photo'}
      </Button>
      <div style={{ display: 'flex', flexWrap: 'wrap', gap: '10px' }}>
        {photos.map((photo, index) => (
          <div key={index} style={{ border: '1px solid #ccc', padding: '10px' }}>
            <img
              src={photo.url}
              alt="pic"
              style={{ width: '100px', height: '100px' }}
              onClick={() => window.open(photo.url, '_blank')}
            />
          </div>
        ))}
      </div>
    </div>

  );
}

export default MedicalHistoryFileManagement;
