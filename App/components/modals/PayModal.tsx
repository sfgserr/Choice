import AnimatedModal from './AnimatedModal.tsx';
import React from 'react';
import GestureBorderedTextInput from '../inputs/GestureBordererdTextInput.tsx';
import TextInputTitle from '../TextInputTitle.tsx';
import {GestureStyledButton} from '../buttons/GestureStyledButton.tsx';
import {Linking, View} from 'react-native';
import {useDependency} from '../../services/Hooks.ts';
import {PaymentService} from '../../services/domain/PaymentService.tsx';

type PayModalProps = {
  isToggled: boolean;
  handlePress: () => void;
}

export default function PayModal({isToggled, handlePress}: PayModalProps) {
  const paymentService = useDependency<PaymentService>('PaymentService');

  const [sumToDeposit, setSumToDeposit] = React.useState('');

  const pay = React.useCallback(async () => {
    const response = await paymentService.createPayment((+sumToDeposit) * 100);

    if (response.result == 'successful') {
      await Linking.openURL(response.content.url!);
    }
  }, [sumToDeposit]);

  return (
    <AnimatedModal
      isToggled={isToggled}
      handlePress={handlePress}
      withBackdrop={true}>
      <View style={{flex: 1, paddingHorizontal: 15, paddingTop: 30}}>
        <TextInputTitle s={'Сумма'} top={0} bottom={5}/>
        <GestureBorderedTextInput
          value={sumToDeposit}
          onChanged={setSumToDeposit}
          placeholder={'Сумма'}
          isError={false}
          isBig={false}
          keyboard={'numeric'}
          isReadonly={false}/>
        <GestureStyledButton
          content={'Перейти к оплате'}
          top={10}
          bottom={5}
          isDisabled={sumToDeposit == ''}
          pressed={pay}
          type={'default'}/>
      </View>
    </AnimatedModal>
  );
}
