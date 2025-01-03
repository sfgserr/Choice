import {OrderRequestModalProps} from '../../types/ComponentTypes.ts';
import AnimatedModal from './AnimatedModal.tsx';
import OrderRequestCard from '../OrderRequestCard.tsx';

export default function OrderRequestModal({isToggled, orderRequest, navigation}: OrderRequestModalProps) {
  return (
    <AnimatedModal
      isToggled={isToggled}
      handlePress={() => {}}
      withBackdrop={false}>
      <OrderRequestCard
        orderRequest={orderRequest}
        navigation={navigation}/>
    </AnimatedModal>
  )
}
