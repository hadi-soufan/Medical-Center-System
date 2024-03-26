import axios from 'axios';
import { updateDoctor } from './doctorStore';

jest.mock('axios');

describe('updateDoctor', () => {
  it('should update the doctor and dispatch the UPDATE_DOCTOR action', async () => {
    const dispatch = jest.fn();
    const updatedDoctor = { id: 1, name: 'John Doe' };

    axios.put.mockResolvedValueOnce();

    await updateDoctor(updatedDoctor)(dispatch);

    expect(axios.put).toHaveBeenCalledWith('http://localhost:5000/api/doctor/1', updatedDoctor);
    expect(dispatch).toHaveBeenCalledWith({ type: 'UPDATE_DOCTOR', payload: updatedDoctor });
  });

  it('should log an error if updating the doctor fails', async () => {
    const dispatch = jest.fn();
    const updatedDoctor = { id: 1, name: 'John Doe' };
    const error = new Error('Failed to update doctor');

    axios.put.mockRejectedValueOnce(error);

    await updateDoctor(updatedDoctor)(dispatch);

    expect(console.log).toHaveBeenCalledWith('Error updating doctor: ', error);
  });
});
