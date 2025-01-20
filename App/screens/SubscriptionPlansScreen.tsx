import React from 'react';
import {
  View,
  Text,
  StyleSheet,
  SafeAreaView,
  FlatList,
} from 'react-native';
import {SubscriptionScreenProps} from '../types/NavigationTypes.ts';
import {StyledButton} from '../components/buttons/StyledButton.tsx';
import SuccessfulRequestModal from '../components/modals/SuccessfulRequestModal.tsx';
import {useDependency} from '../services/Hooks.ts';
import {SubscriptionPaymentService} from '../services/domain/SubscriptionPaymentService.ts';

interface Plan {
  id: number;
  plan: string;
  title: string;
  price: number;
  duration: string;
  features: string[];
}

const plans: Plan[] = [
  {
    id: 0,
    plan: 'Month',
    title: 'Начальный',
    price: 800,
    duration: 'месяц',
    features: [],
  },
  {
    id: 1,
    plan: 'HalfYear',
    title: 'Средний',
    price: 4320,
    duration: '6 месяцев',
    features: ['Экономия 10% по сравнению с 1-ым планом'],
  },
  {
    id: 2,
    plan: 'Year',
    title: 'Премиум',
    price: 7776,
    duration: '12 месяцев',
    features: ['Экономия 19% по сравнению с 1-ым планом', 'Экономия 10% по сравнению со 2-ым планом'],
  },
];

export default function SubscriptionPlansScreen({route, navigation}: SubscriptionScreenProps) {
  const subscriptionPaymentService = useDependency<SubscriptionPaymentService>('SubscriptionPaymentService');

  const [isToggled, setIsToggled] = React.useState(false);
  const [planIndex, setPlanIndex] = React.useState(-1);

  const renderPlan = ({item}: {item: Plan}) => (
    <View style={styles.card}>
      <Text style={styles.title}>{item.title}</Text>
      <Text style={styles.price}>{`${item.price}\u20BD`}</Text>
      <Text style={styles.duration}>{item.duration}</Text>
      <View style={styles.featuresContainer}>
        {item.features.map((feature, index) => (
          <Text key={index} style={styles.feature}>
            • {feature}
          </Text>
        ))}
      </View>
      <StyledButton
        content={'Выбрать'}
        top={0}
        bottom={0}
        isDisabled={false}
        pressed={() => {
          setPlanIndex(item.id);
          setIsToggled(true);
        }}
      />
    </View>
  );

  return (
    <SafeAreaView style={styles.container}>
      <Text style={styles.header}>Выберите план</Text>
      <FlatList
        data={plans}
        renderItem={renderPlan}
        showsVerticalScrollIndicator={false}
        keyExtractor={(item) => item.plan}
        contentContainerStyle={styles.list}
      />
      <SuccessfulRequestModal
        isToggled={isToggled}
        handlePress={async () => {
          await subscriptionPaymentService.buy(plans[planIndex].plan);

          setIsToggled(prev => !prev);

          navigation.navigate('PaySubscription', {price: plans[planIndex].price});
        }}
        title={'Создать заказ?'}
        text={''}/>
    </SafeAreaView>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#f5f5f5',
    padding: 16,
  },
  header: {
    fontSize: 24,
    fontWeight: 'bold',
    textAlign: 'center',
    marginBottom: 20,
  },
  list: {
    paddingBottom: 20,
  },
  card: {
    backgroundColor: '#ffffff',
    borderRadius: 12,
    padding: 20,
    marginBottom: 20,
    shadowColor: '#000',
    shadowOpacity: 0.1,
    shadowRadius: 4,
    shadowOffset: { width: 0, height: 2 },
    elevation: 3,
  },
  title: {
    fontSize: 20,
    fontWeight: 'bold',
    marginBottom: 8,
  },
  price: {
    fontSize: 18,
    color: '#4caf50',
    marginBottom: 4,
  },
  duration: {
    fontSize: 16,
    color: '#757575',
    marginBottom: 12,
  },
  featuresContainer: {
    marginBottom: 16,
  },
  feature: {
    fontSize: 14,
    color: '#333',
    marginBottom: 4,
  },
  button: {
    backgroundColor: '#6200ea',
    padding: 12,
    borderRadius: 8,
    alignItems: 'center',
  },
  buttonText: {
    color: '#ffffff',
    fontSize: 16,
    fontWeight: 'bold',
  },
});

