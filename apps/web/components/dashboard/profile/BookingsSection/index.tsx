import { Box, Center, Spinner, VStack, Text, Button } from "@chakra-ui/react"
import { useState } from "react"
import InfiniteScroll from "react-infinite-scroll-component"
import { FormattedMessage } from "react-intl"
import {
  BookingRequestDto,
  BookingRequestStatus,
} from "../../../../api/tatuazApi"
import BookingRequestModal from "../BookingRequestModal"

const formatStatus = (status: BookingRequestStatus) => {
  switch (status) {
    case BookingRequestStatus.Pending:
      return (
        <FormattedMessage
          defaultMessage="Oczekuje na potwierdzenie"
          id="QGrxT+"
        />
      )
    case BookingRequestStatus.Accepted:
      return <FormattedMessage defaultMessage="Zaakceptowano" id="mpOu7G" />
    case BookingRequestStatus.Rejected:
      return <FormattedMessage defaultMessage="Odrzucono" id="sQi8RU" />
    default:
      return <FormattedMessage defaultMessage="Nieznany status" id="GCda7O" />
  }
}

const BookingRequestsSection = ({
  myBookingRequests,
  fetchNextPage,
  hasNextPage,
  isIncoming = false,
  isLoading,
}: {
  myBookingRequests: BookingRequestDto[]
  fetchNextPage: () => void
  hasNextPage: boolean
  isIncoming?: boolean
  isLoading: boolean
}) => {
  const [isBookingRequestModalOpen, setIsBookingRequestModalOpen] =
    useState(false)

  const handleBookingRequestModalOpen = () => setIsBookingRequestModalOpen(true)

  if (isLoading)
    return (
      <Center mb={10} mt={10}>
        <Spinner size="xl" />
      </Center>
    )

  return (
    <InfiniteScroll
      dataLength={myBookingRequests.length}
      endMessage={
        <Center mb={10} mt={10}>
          <FormattedMessage
            defaultMessage="Nie ma więcej rezerwacji do wyświetlenia 😔"
            id="Aru1hn"
          />
        </Center>
      }
      hasMore={!!hasNextPage}
      loader={
        <Center mb={10} mt={10}>
          <Spinner />
        </Center>
      }
      next={fetchNextPage}
    >
      <VStack gap={8} width={{ base: "100%" }}>
        {myBookingRequests.map((booking) => (
          <Box
            key={booking.id}
            borderRadius="lg"
            borderWidth="1px"
            overflow="hidden"
          >
            <VStack alignItems="flex-start" p={4} spacing={2}>
              <Text fontWeight="bold">{booking.clientName}</Text>
              <Text>{booking.comment}</Text>
              <Text>
                {new Date(booking.start).toLocaleString()} -{" "}
                {new Date(booking.end).toLocaleString()}
              </Text>
              <Text>Status: {formatStatus(booking.status)}</Text>
              {isIncoming && (
                <>
                  <Button
                    colorScheme="primary"
                    onClick={handleBookingRequestModalOpen}
                  >
                    <FormattedMessage defaultMessage="Odpowiedz" id="mGv3OR" />
                  </Button>
                  <BookingRequestModal
                    bookingRequestId={booking.id}
                    isOpen={isBookingRequestModalOpen}
                    onClose={() => setIsBookingRequestModalOpen(false)}
                  />
                </>
              )}
            </VStack>
          </Box>
        ))}
      </VStack>
    </InfiniteScroll>
  )
}

export default BookingRequestsSection
