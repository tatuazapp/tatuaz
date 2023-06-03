import { Text, Tab, TabList, TabPanel, TabPanels, Tabs } from "@chakra-ui/react"
import { useInfiniteQuery } from "@tanstack/react-query"
import { useState } from "react"
import { FormattedMessage } from "react-intl"
import { api } from "../../../../api/apiClient"
import { queryKeys } from "../../../../api/queryKeys"
import {
  BookingRequestStatus,
  BookingRequestDto,
} from "../../../../api/tatuazApi"
import { tabIndexToBookingStatus } from "../../../../pages/dashboard/bookings"
import BookingRequestsSection from "../BookingsSection"

export type UserProfile = {
  username?: string
  foregroundPhotoUri?: string | null
  backgroundPhotoUri?: string | null
  bio?: string | null
  city?: string | null
  artist?: boolean
}

const BOOKINGS_PAGE_SIZE = 1000

const MyBookings = () => {
  const [bookingStatus, setBookingStatus] = useState(
    BookingRequestStatus.Pending
  )

  const {
    data: bookingRequests,
    fetchNextPage,
    hasNextPage,
    isLoading,
  } = useInfiniteQuery(
    [queryKeys.listMyBookingRequests, bookingStatus],
    ({ pageParam = 1 }) =>
      api.booking.listMyBookingRequests({
        status: bookingStatus,
        pageNumber: pageParam,
        pageSize: BOOKINGS_PAGE_SIZE,
      }),
    {
      getNextPageParam: (lastPage) => {
        const nextPage = lastPage.pageNumber ?? 0
        return nextPage < lastPage.totalPages ? nextPage + 1 : undefined
      },
    }
  )

  // TODO: yeah, whatever
  const bookingRequestsWithAssertedType = bookingRequests as unknown as {
    pages: {
      success: boolean
      value: {
        data: BookingRequestDto[]
        pageNumber: number
        totalCount: number
        pageSize: number
        totalPages: number
      }
    }[]
  }

  const myBookingRequests =
    bookingRequestsWithAssertedType?.pages?.flatMap(
      (page) => page?.value.data ?? []
    ) ?? []

  return (
    <Tabs
      colorScheme="primary"
      onChange={(index) => setBookingStatus(tabIndexToBookingStatus(index))}
    >
      <Text fontSize="3xl" fontWeight="bold" mb={8}>
        <FormattedMessage defaultMessage="Moje rezerwacje" id="pDjHc+" />
      </Text>
      <TabList>
        <Tab>Oczekujące</Tab>
        <Tab>Zaakceptowane</Tab>
        <Tab>Odrzucone</Tab>
      </TabList>
      <TabPanels>
        <TabPanel>
          <BookingRequestsSection
            fetchNextPage={fetchNextPage}
            hasNextPage={!!hasNextPage}
            isLoading={isLoading}
            myBookingRequests={myBookingRequests}
          />
        </TabPanel>
        <TabPanel>
          <Text>
            <BookingRequestsSection
              fetchNextPage={fetchNextPage}
              hasNextPage={!!hasNextPage}
              isLoading={isLoading}
              myBookingRequests={myBookingRequests}
            />
          </Text>
        </TabPanel>
        <TabPanel>
          <Text>
            <BookingRequestsSection
              fetchNextPage={fetchNextPage}
              hasNextPage={!!hasNextPage}
              isLoading={isLoading}
              myBookingRequests={myBookingRequests}
            />
          </Text>
        </TabPanel>
      </TabPanels>
    </Tabs>
  )
}

export default MyBookings
