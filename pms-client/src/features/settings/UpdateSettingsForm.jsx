import { useSettings } from './useSettings';

import { useUpdateSetting } from './useUpdateSetting';
import Form from '../../ui/Form';
import FormRow from '../../ui/FormRow';
import Input from '../../ui/Input';
import Spinner from '../../ui/Spinner';

function UpdateSettingsForm() {
  const { isLoading, error, settings: {
    minBookingLength, maxBookingLength, maxGuestesPerBooking, breakfastPrice
  } = {} } = useSettings();

  const { isUpdating, updateSetting } = useUpdateSetting();

  function handleUpdate(e, field) {
    const { value } = e.target;

    if (!value) return

    updateSetting({ [field]: value });
  }

  if (isLoading) return <Spinner />

  return (
    <Form>
      <FormRow label='Minimum nights/booking'>
        <Input type='number' id='min-nights' defaultValue={minBookingLength} onBlur={e => handleUpdate(e, 'minBookingLength')} />
      </FormRow>

      <FormRow label='Maximum nights/booking'>
        <Input type='number' id='max-nights' defaultValue={maxBookingLength} onBlur={e => handleUpdate(e, 'maxBookingLength')} />
      </FormRow>

      <FormRow label='Maximum guests/booking'>
        <Input type='number' id='max-guests' defaultValue={maxGuestesPerBooking} onBlur={e => handleUpdate(e, 'maxGuestesPerBooking')} />
      </FormRow>

      <FormRow label='Breakfast price'>
        <Input type='number' id='breakfast-price' defaultValue={breakfastPrice} onBlur={e => handleUpdate(e, 'breakfastPrice')} />
      </FormRow>
    </Form>
  );
}

export default UpdateSettingsForm;
