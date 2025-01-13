import React, { useState, useEffect } from "react";
import { View, Text, TextInput, TouchableOpacity, StyleSheet, Alert } from "react-native";
import {PaySubscriptionScreenProps} from '../types/NavigationTypes.ts';

export default function PaySubscriptionScreen({route, navigation}: PaySubscriptionScreenProps){
  const [timeLeft, setTimeLeft] = useState(600); // 10 minutes in seconds
  const [cardDetails, setCardDetails] = useState({
    name: "",
    number: "",
    expiry: "",
    cvv: "",
  });
  const [isValid, setIsValid] = useState(false);

  // Timer logic
  useEffect(() => {
    const timer = setInterval(() => {
      setTimeLeft((prev) => Math.max(prev - 1, 0));
    }, 1000);

    if (timeLeft === 0) {
      Alert.alert("Time Expired", "Your payment session has expired.");
      clearInterval(timer);
    }

    return () => clearInterval(timer);
  }, [timeLeft]);

  // Format time as MM:SS
  const formatTime = (seconds) => {
    const minutes = Math.floor(seconds / 60);
    const secs = seconds % 60;
    return `${minutes.toString().padStart(2, "0")}:${secs.toString().padStart(2, "0")}`;
  };

  // Input change handler
  const handleChange = (field, value) => {
    setCardDetails((prev) => ({ ...prev, [field]: value }));

    // Simple validation for demo purposes
    const { name, number, expiry, cvv } = { ...cardDetails, [field]: value };
    if (name && number.length === 16 && expiry.length === 5 && cvv.length === 3) {
      setIsValid(true);
    } else {
      setIsValid(false);
    }
  };

  // Handle payment submission
  const handlePayment = () => {
    if (!isValid) {
      Alert.alert("Invalid Details", "Please fill in all fields correctly.");
      return;
    }

    Alert.alert("Payment Successful", "Your payment has been processed.");
  };

  return (
    <View style={styles.container}>
      <Text style={styles.header}>Subscription Payment</Text>

      {/* Cardholder Name */}
      <TextInput
        style={styles.input}
        placeholder="Cardholder Name"
        value={cardDetails.name}
        onChangeText={(value) => handleChange("name", value)}
      />

      {/* Card Number */}
      <TextInput
        style={styles.input}
        placeholder="Card Number (16 digits)"
        keyboardType="numeric"
        value={cardDetails.number}
        onChangeText={(value) => handleChange("number", value)}
        maxLength={16}
      />

      {/* Expiry Date and CVV */}
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

      {/* Timer */}
      <Text style={styles.timer}>⏳ Time Remaining: {formatTime(timeLeft)}</Text>

      {/* Pay Now Button */}
      <TouchableOpacity
        style={[styles.button, isValid ? styles.buttonActive : styles.buttonDisabled]}
        disabled={!isValid}
        onPress={handlePayment}
      >
        <Text style={styles.buttonText}>Pay Now</Text>
      </TouchableOpacity>

      <Text style={styles.footer}>
        Note: Please complete your payment within 10 minutes, or your session will expire.
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
    backgroundColor: "#4CAF50",
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
