import AnimatedModal from './AnimatedModal.tsx';
import React from 'react';
import GestureBorderedTextInput from '../inputs/GestureBordererdTextInput.tsx';
import TextInputTitle from '../TextInputTitle.tsx';
import {GestureStyledButton} from '../buttons/GestureStyledButton.tsx';
import {View} from 'react-native';
import {useDependency} from '../../services/Hooks.ts';
import {PaymentService} from '../../services/domain/PaymentService';

type PayoutModalProps = {
  isToggled: boolean;
  handlePress: () => void;
}

export default function PayoutModal({isToggled, handlePress}: PayoutModalProps) {
  const paymentService = useDependency<PaymentService>('PaymentService');

  const [sumToDeposit, setSumToDeposit] = React.useState<string>('');
  const [cardNumber, setCardNumber] = React.useState<string>('');

  const payOut = React.useCallback(async () => {
    const response = await paymentService.createPayout(cardNumber, (+sumToDeposit) * 100);

    if (response.result == 'successful') {
      handlePress();
    }
  }, [sumToDeposit]);

  return (
    <AnimatedModal
      isToggled={isToggled}
      handlePress={handlePress}
      withBackdrop={true}>
      <View style={{flex: 1, paddingHorizontal: 15, paddingTop: 30}}>
        <TextInputTitle s={'Номер карты'} top={0} bottom={5}/>
        <GestureBorderedTextInput
          value={cardNumber}
          onChanged={setCardNumber}
          placeholder={'Номер карты'}
          isError={false}
          isBig={false}
          keyboard={'numeric'}
          isReadonly={false}/>
        <TextInputTitle s={'Сумма'} top={20} bottom={5}/>
        <GestureBorderedTextInput
          value={sumToDeposit}
          onChanged={setSumToDeposit}
          placeholder={'Сумма'}
          isError={false}
          isBig={false}
          keyboard={'numeric'}
          isReadonly={false}/>
        <GestureStyledButton
          content={'Снять'}
          top={10}
          bottom={5}
          isDisabled={sumToDeposit == ''}
          pressed={payOut}
          type={'default'}/>
      </View>
    </AnimatedModal>
  );
}
