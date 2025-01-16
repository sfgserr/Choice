import React, { useState, useEffect } from "react";
import { View, Text, TextInput, TouchableOpacity, StyleSheet, Alert } from "react-native";
import {PaySubscriptionScreenProps} from '../types/NavigationTypes.ts';
import {SubscriptionPayment} from '../types/DomainTypes.ts';
import {AuthContext} from '../App.tsx';

export default function PaySubscriptionScreen({route, navigation}: PaySubscriptionScreenProps){
  const { changeState } = React.useContext(AuthContext);

  const [timeLeft, setTimeLeft] = useState(600); // 10 minutes in seconds
  const [subscriptionPayment, setSubscriptionPayment] = React.useState<SubscriptionPayment>(null);

  const [cardDetails, setCardDetails] = useState({
    name: "",
    number: "",
    expiry: "",
    cvv: "",
  });
  const [isValid, setIsValid] = useState(false);

  useEffect(() => {
    const getPayment = async () => {
      let response = await route.params.subscriptionPaymentService.get(changeState);

      if (response.content != null) {
        setSubscriptionPayment(response.content);
        setTimeLeft((new Date(response.content.expirationDate).getTime() - Date.now()) / 1000);
      }
    }
    getPayment();
  }, []);

  useEffect(() => {
    const timer = setInterval(() => {
      setTimeLeft((prev) => Math.max(prev - 1, 0));
    }, 1000);

    if (timeLeft === 0) {
      Alert.alert("Время вышло", "Срок оплаты истек");
      clearInterval(timer);
    }

    return () => clearInterval(timer);
  }, [timeLeft]);

  const formatTime = (seconds) => {
    const minutes = Math.floor(seconds / 60);
    const secs = seconds % 60;
    return `${minutes.toString().padStart(2, "0")}:${secs.toString().padStart(2, "0")}`;
  };

  // Input change handler
  const handleChange = (field, value) => {
    setCardDetails((prev) => ({ ...prev, [field]: value }));

    const { name, number, expiry, cvv } = { ...cardDetails, [field]: value };
    if (name && number.length === 16 && expiry.length === 5 && cvv.length === 3) {
      setIsValid(true);
    } else {
      setIsValid(false);
    }
  };

  const handlePayment = () => {
    if (!isValid) {
      Alert.alert("Неправильные данные", "Пожалуйста заполните все поля корректно");
      return;
    }

    Alert.alert("Оплата успешна", "Ваш платеж прошел");
  };

  const getPlanTitle = (period: string) => {
    switch (period) {
      case 'Month':
        return 'Начальный';
      case 'HalfYear':
        return 'Средний';
      case 'Year':
        return 'Премиум';
      default:
        return '';
    }
  }

  return (
    <View style={styles.container}>
      <Text style={styles.header}>
        {subscriptionPayment == null ? 'Оплата подписки' : `Оплата подписки: ${getPlanTitle(subscriptionPayment.period)}`}
      </Text>
      <TextInput
        style={styles.input}
        placeholder="Имя владельца карты"
        value={cardDetails.name}
        onChangeText={(value) => handleChange("name", value)}
      />
      <TextInput
        style={styles.input}
        placeholder="Номер карты"
        keyboardType="numeric"
        value={cardDetails.number}
        onChangeText={(value) => handleChange("number", value)}
        maxLength={16}
      />
      <View style={styles.row}>
        <TextInput
          style={[styles.input, styles.halfInput]}
          placeholder="MM/YY"
          keyboardType="numeric"
          value={cardDetails.expiry}
          onChangeText={(value) => handleChange("expiry", value)}
          maxLength={5}
        />
        <TextInput
          style={[styles.input, styles.halfInput]}
          placeholder="CVV"
          keyboardType="numeric"
          value={cardDetails.cvv}
          onChangeText={(value) => handleChange("cvv", value)}
          maxLength={3}
        />
      </View>
      <Text style={styles.timer}>⏳ Оставшееся времени: {formatTime(timeLeft)}</Text>
      <TouchableOpacity
        style={[styles.button, isValid ? styles.buttonActive : styles.buttonDisabled]}
        disabled={!isValid}
        onPress={handlePayment}
      >
        <Text style={styles.buttonText}>Оплатить</Text>
      </TouchableOpacity>
      <Text style={styles.footer}>
        Заметка: Пожалуйста, завершите оплату в течение 10 минут, иначе ваш сеанс будет истечен.
      </Text>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 20,
    backgroundColor: "#f9f9f9",
  },
  header: {
    fontSize: 24,
    fontWeight: "bold",
    marginBottom: 20,
    textAlign: "center",
  },
  input: {
    height: 50,
    borderColor: "#ccc",
    borderWidth: 1,
    borderRadius: 8,
    paddingHorizontal: 10,
    marginBottom: 15,
    backgroundColor: "#fff",
  },
  row: {
    flexDirection: "row",
    justifyContent: "space-between",
  },
  halfInput: {
    width: "48%",
  },
  timer: {
    fontSize: 18,
    fontWeight: "bold",
    textAlign: "center",
    marginVertical: 20,
    color: "#333",
  },
  button: {
    height: 50,
    borderRadius: 8,
    justifyContent: "center",
    alignItems: "center",
  },
  buttonActive: {
    backgroundColor: "#2D81E0",
  },
  buttonDisabled: {
    backgroundColor: "#ccc",
  },
  buttonText: {
    color: "#fff",
    fontSize: 18,
    fontWeight: "bold",
  },
  footer: {
    marginTop: 20,
    fontSize: 14,
    textAlign: "center",
    color: "#666",
  },
});
